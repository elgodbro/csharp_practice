using Exc1;

NotificationFactory emailFactory = new EmailFactory();
NotificationFactory smsFactory = new SMSFactory();
NotificationFactory pushFactory = new PushFactory();

var email = emailFactory.CreateNotification();
var sms = smsFactory.CreateNotification();
var push = pushFactory.CreateNotification();

email.Send("Новое письмо!");
sms.Send("Ваш код подтверждения: 1234");
push.Send("У вас новое сообщение");