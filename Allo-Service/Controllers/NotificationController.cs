using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Allo_Service.Models;
using Allo_Service.ViewModel;

namespace Allo_Service.Controllers
{
    public class NotificationController : Controller
    {
        private readonly MyContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationController(MyContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;

        }
        public IActionResult List()
        {
            var notifications = _context.Notifications.ToList();
            return View(notifications);
        }
        //public IActionResult Delete(int id)
        //{
        //    var notification = _context.Notifications.Find(id);
        //    if (notification == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Notifications.Remove(notification);
        //    _context.SaveChanges();

        //    return RedirectToAction(nameof(List));
        //}
        //private bool NotificationExists(int id)
        //{
        //    return _context.Notifications.Any(e => e.NotificationId == id);
        //}
        public IActionResult Add()
        {
            return View(new NotificationVM()); // Create new view model
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(NotificationVM notificationVM)
        {
            if (ModelState.IsValid)
            {
                // Map view model data to Notification model (without UserId)
                var newNotification = new Notification
                {
                    Titre = notificationVM.Titre,
                    Contenu = notificationVM.Contenu,
                    UserType = notificationVM.UserType,
                    DateNotif = DateTime.Now
                };

                _context.Notifications.Add(newNotification);
                await _context.SaveChangesAsync();

                if (newNotification.UserType == "All")
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", newNotification);
                }
                else
                {
                    await _hubContext.Clients.Group(newNotification.UserType).SendAsync("ReceiveNotification", newNotification);
                }

                return RedirectToAction(nameof(List));
            }

            // Re-populate view model with form data on validation error
            return View(notificationVM);
        }

        public IActionResult GetLastNotifications()
        {
            var usertype = HttpContext.Session.GetString("UserType");
        
            var notifications = _context.Notifications
                .OrderByDescending(n => n.DateNotif)
                .Where(n => n.UserType == usertype|| n.UserType == "All")
                .Take(5)
                .ToList();

            return Json(notifications);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync(); // Use async method for saving changes

            return RedirectToAction(nameof(List));
        }
    }
}
