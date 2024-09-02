using System.Collections.Generic;

namespace Cgs.TechCursos.Domain.Notifications
{
    /// <summary>
    /// Classe responsavél pelo gerenciamento das notificações
    /// </summary>
    public abstract class Notifiable
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        protected Notifiable()
        {
            _notifications = new List<Notification>();
        }

        protected void AddNotification(string property, string message)
        {
            _notifications.Add(new Notification(property, message));
        }

        protected void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public bool IsValid => _notifications.Count == 0;
    }
}
