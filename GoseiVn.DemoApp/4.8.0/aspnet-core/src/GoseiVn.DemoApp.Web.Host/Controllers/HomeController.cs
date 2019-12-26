using Abp;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using Abp.UI;
using GoseiVn.DemoApp.Controllers;
using GoseiVn.DemoApp.Estimates;
using GoseiVn.DemoApp.Models;
using GoseiVn.DemoApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace GoseiVn.DemoApp.Web.Host.Controllers
{
    public class HomeController : DemoAppControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IEstimateAppService _estimateAppService;

        public HomeController(INotificationPublisher notificationPublisher, IEstimateAppService estimateAppService)

        {
            _notificationPublisher = notificationPublisher;
            _estimateAppService = estimateAppService;
        }

        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: NotificationSeverity.Info,
                userIds: new[] { defaultTenantAdmin, hostAdmin }
            );

            return Content("Sent notification: " + message);
        }



        public ActionResult Image(int id)
        {
            Images imageObject = _estimateAppService.GetImageById(id);

            if (imageObject == null)
            {
                throw new UserFriendlyException(L("ImageNotFound"));
            }

            var fileBytes = System.IO.File.ReadAllBytes(imageObject.ImageUrl);

            return File(fileBytes, MimeTypeMap.GetMimeType(Path.GetExtension(imageObject.ImageUrl)), Path.GetFileName(imageObject.ImageUrl));
        }

        public ActionResult Delete(int id)
        {
            Images imageObject = _estimateAppService.GetImageById(id);

            if (imageObject == null)
            {
                throw new UserFriendlyException(L("ImageNotFound"));
            }

            System.IO.File.Delete(imageObject.ImageUrl);

            return null;
        }
    }
}