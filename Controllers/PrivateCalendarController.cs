using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliNote.DTOs.PrivateCalendar;
using PoliNote.DTOs.PublicCalendar;
using PoliNote.Repositories;
using PoliNote.Services.auth;
using PoliNote.Services.Auth;
using PoliNote.Services.Calendar;

namespace PoliNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateCalendarController : ControllerBase
    {
        private readonly PrivateCalendarRepository _privateCalendarRepo;
        private readonly AuthService _authService;
        private readonly IsOwnerService _isOwnerService;
        private readonly PrivateEventValidator _eventValidator;
        private readonly DateValidator _dateValidator;

        public PrivateCalendarController(
            PrivateCalendarRepository privateCalendarRepo,
            AuthService authService,
            IsOwnerService isOwnerService,
            PrivateEventValidator eventValidator,
            DateValidator dateValidator
            )
        {
            _privateCalendarRepo = privateCalendarRepo;
            _authService = authService;
            _isOwnerService = isOwnerService;
            _eventValidator = eventValidator;
            _dateValidator = dateValidator;
        }


    }
}
