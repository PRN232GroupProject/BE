using BusinessObjects.DTO;
using BusinessObjects.DTO.Chapter;
using BusinessObjects.DTO.User;
using BusinessObjects.DTO.User.Auth;
using BusinessObjects.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    [Mapper]
    public partial class MapperlyMapper : IMapperlyMapper
    {
        // User to UserDto mapping
        [MapProperty(nameof(User.Role.Name), nameof(UserDTO.Role))]
        public partial UserDTO UserToUserDto(User user);

        // User to LoginResponse mapping - only Role name
        [MapProperty(nameof(User.Role.Name), nameof(LoginResponse.Role))]
        public partial LoginResponse UserToLoginResponse(User user);

        // RegisterRequest to User mapping
        public partial User RegisterRequestToUser(RegisterRequest request);

        [MapProperty(nameof(UserRequestDTO.Role), nameof(User.RoleId))]
        public partial User RequestDTOToUser(UserRequestDTO request);

        // Chapter mappings

        [MapProperty(nameof(CreateChapterRequest.ChapterName), nameof(Chapter.Name))]
        public partial Chapter CreateChapterRequestToChapter(CreateChapterRequest request);
        [MapProperty(nameof(Chapter.Name), nameof(ChapterResponse.ChapterName))]
        [MapProperty(nameof(Chapter.Id), nameof(ChapterResponse.ChapterId))]
        public partial ChapterResponse ChapterToChapterResponse(Chapter chapter);
        public partial List<ChapterResponse> ChaptersToChapterResponses(List<Chapter> chapters);

        // Update mapping - map only the fields that should be updated

        [MapProperty(nameof(UpdateChapterRequest.ChapterName), nameof(Chapter.Name))]
        [MapperIgnoreTarget(nameof(Chapter.Lessons))]
        public partial void UpdateChapterFromRequest(UpdateChapterRequest request, Chapter chapter);

    }
}