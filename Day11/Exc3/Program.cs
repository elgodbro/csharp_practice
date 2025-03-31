using Exc3;

var channel = new YouTubeChannel(Faker.User.Username());

var user1 = new User(Faker.User.Username());
var user2 = new User(Faker.User.Username());
var user3 = new User(Faker.User.Username());

channel.Subscribe(user1);
channel.Subscribe(user2);
channel.Subscribe(user3);

channel.UploadVideo(Faker.Lorem.Sentence(5));

channel.Unsubscribe(user3);

channel.UploadVideo(Faker.Lorem.Sentence(5));