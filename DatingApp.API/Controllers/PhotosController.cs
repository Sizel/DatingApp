using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data.Models;
using DatingApp.Data.DTOs.Photos;
using DatingApp.Data.Models;
using DatingApp.Data.Repos;
using DatingApp.Misc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.Controllers
{
    [Route("api/users/{id}/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly IPhotoRepository photosRepo;
        private readonly IMapper mapper;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private Cloudinary cloudinary;

        public PhotosController(IUserRepository userRepo, IPhotoRepository photosRepo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this.userRepo = userRepo;
            this.photosRepo = photosRepo;
            this.mapper = mapper;
            this.cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );

            cloudinary = new Cloudinary(account);
        }

        [HttpGet("photoId", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id, int photoId)
        {
            var userWithPhotos = await userRepo.GetUserWithPhotos(id);

            var photo = userWithPhotos.Photos.FirstOrDefault(p => p.PhotoId == photoId);

            var photoToReturnDto = mapper.Map<PhotoToReturnDTO>(photo);

            return Ok(photoToReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> SavePhoto(int id, [FromForm]PhotoForCreationDTO photoForCreationDto)
        {
            var idFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (idFromToken != id)
            {
                return Unauthorized();
            }

            var userFromRepo = await userRepo.GetUserWithPhotos(id);

            if (userFromRepo == null)
            {
                BadRequest("No such user");
            }

            var file = photoForCreationDto.File;

            if (file.Length <= 0)
            {
                return BadRequest("No file");
            }

            var uploadResult = new ImageUploadResult();

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };

                uploadResult = cloudinary.Upload(uploadParams);
            }

            var photoToSave = new Photo()
            {
                Url = uploadResult.Uri.ToString(),
                PublicId = uploadResult.PublicId,
                DateAdded = DateTime.Now
            };

            if (!userFromRepo.Photos.Any(p => p.IsMain == true))
            {
                photoToSave.IsMain = true;
            }
            else
            {
                photoToSave.IsMain = false;
            }

            userFromRepo.Photos.Add(photoToSave);
            await userRepo.SaveAll();

            var photoToReturn = mapper.Map<PhotoToReturnDTO>(photoToSave);

            return CreatedAtRoute("GetPhoto", new { id, photoToSave.PhotoId }, photoToReturn);
        }

        [HttpPut("main/{photoId}")]
        public async Task<IActionResult> SetMainPhoto(int id, int photoId)
        {
            var idFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (idFromToken != id)
            {
                return Unauthorized();
            }

            var userFromRepo = await userRepo.GetUserWithPhotos(id);

            if (userFromRepo == null)
            {
                return BadRequest("No such user");
            }

            var currentMainPhoto = userFromRepo.Photos.FirstOrDefault(p => p.IsMain);
            if (currentMainPhoto != null)
            {
                currentMainPhoto.IsMain = false;
            }

            var requestedMainPhoto = userFromRepo.Photos.FirstOrDefault(p => p.PhotoId == photoId);
            if (requestedMainPhoto == null)
            {
                return BadRequest("No such photo");
            }
            else
            {
                requestedMainPhoto.IsMain = true;
                await userRepo.SaveAll();
                return NoContent();
            }
        }

        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhoto(int id, int photoId)
        {
            var idFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (idFromToken != id)
            {
                return Unauthorized();
            }

            var userFromRepo = await userRepo.GetUserWithPhotos(id);

            if (userFromRepo == null)
            {
                return BadRequest("No such user");
            }

            var photoToDelete = userFromRepo.Photos.FirstOrDefault(p => p.PhotoId == photoId);
            if (photoToDelete == null)
            {
                return BadRequest("No such photo");
            }

            if (photoToDelete.IsMain)
            {
                return BadRequest("You can't delete the main photo");
            }

            if (photoToDelete.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoToDelete.PublicId);

                var result = cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    photosRepo.Remove(photoToDelete);
                }
            }

            photosRepo.Remove(photoToDelete);

            await photosRepo.SaveAll();

            return Ok();
        }
    }
}