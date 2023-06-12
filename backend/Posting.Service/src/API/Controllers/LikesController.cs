using Application.DTOs;
using Application.Services.LikesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : BaseApiController
    {
        public readonly ILikesService _likeService;
        public LikesController(ILikesService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{id}/like")]
        public async Task<ActionResult<PostDTO>> LikePost(Guid id)
        {
            return HandleResult(await _likeService.LikePost(id));
        }

        [HttpPost("{id}/unlike")]
        public async Task<ActionResult<PostDTO>> UnlikePost(Guid id)
        {
            return HandleResult(await _likeService.UnlikePost(id));
        }
    }
}