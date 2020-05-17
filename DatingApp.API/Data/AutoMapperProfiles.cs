using AutoMapper;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;
using DatingApp.Data.DTOs;
using DatingApp.Data.DTOs.Messages;
using DatingApp.Data.Models;
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

			CreateMap<User, DetailedUserDTO>()
				.ForMember(destDto => destDto.MainPhotoUrl,
					options => options.MapFrom(user => user.Photos.FirstOrDefault(p => p.IsMain).Url))
				.ForMember(destDto => destDto.Age, options =>
					options.MapFrom(user => DateTime.Now.Year - user.DateOfBirth.Year));

			CreateMap<UserForUpdateDTO, User>()
				.ForPath<UserDescription>(destUser => destUser.UserDescription, options =>
					options.MapFrom(srcDto => srcDto.UserDescription));

			CreateMap<UserDescriptionDTO, UserDescription>()
				.ForMember(descr => descr.Description,
					options => options.MapFrom(descrDto => descrDto.Description))
				.ForMember(descr => descr.Interests,
					options => options.MapFrom(descrDto => descrDto.Interests));

			CreateMap<UserForRegisterDTO, User>()
				.ForMember(u => u.Password,
					options => options.Ignore())
				.ForMember(u => u.Created,
					options => options.MapFrom(userForRegisterdto => DateTime.Now))
				.ForMember(u => u.LastActive,
					options => options.MapFrom(userForRegisterdto => DateTime.Now));

			CreateMap<MessageForCreationDTO, Message>()
				.ForMember(m => m.DateSent,
					options => options.MapFrom(mDto => DateTime.Now));

			CreateMap<Message, MessageToReturnDto>()
				.ForMember(mDto => mDto.RecipientPhotoUrl,
					options => options.MapFrom(m => m.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url))
				.ForMember(mDto => mDto.SenderPhotoUrl,
					options => options.MapFrom(m => m.Sender.Photos.FirstOrDefault(p => p.IsMain).Url));
		}
	}
}
