using AutoMapper;
using DatingApp.API.Data.Models;
using DatingApp.Data.DTOs;
using System;
using System.Linq;

namespace DatingApp.Data
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<User, UserForListDTO>()
				.ForMember(destDto => destDto.MainPhotoUrl, options =>
					options.MapFrom(user => user.Photos.FirstOrDefault(p => p.IsMain).Url))
				.ForMember(destDto => destDto.Age, options =>
					options.MapFrom(user => DateTime.Now.Year - user.DateOfBirth.Year));

			CreateMap<User, DetailedUserDTO>().ForMember
			(
				destDto => destDto.MainPhotoUrl,
				options => options.MapFrom(user => user.Photos.FirstOrDefault(p => p.IsMain).Url)
			);
		}
	}
}
