using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Models.Video;

public record VideoToCreate (
    string Name,
    //bool IsDeleted,
  //  int Views,
  //  int Likes,
    string Description,
    //long UserId,
   // TimeSpan Duration,
   // bool IsBanned,
    long IdVideo,
   // int CommentsAmount,
    List<TagToCreate> Tags
    );