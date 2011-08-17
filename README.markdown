The MadMmimi API Wrapper for .NET 4.0
=====================================

Stop trying to send emails from your app - they're far too important. Instead, consider a 3rd party service like MadMimi. You client or boss can change wording and format all they like - you just tell MadMimi what to send and when. Stats and clicks are tracked... it's a great solution.

How To Install It?
------------------
Reference the MadMimi.dll in your project.

How Do You Use It?
------------------
It's pretty simple to use - this wrapper does nothing more than tell MadMimi what to send and to whom:

    var mimi = new MadMimi.Api("my@username.com", "MY_API_KEY");
    var result = mimi.SendMailer("coupon", "rob@wekeroad.com", replacements: new {code = "12345"});

    Console.WriteLine("Message sent: " + result);

That's it. The wrapper also supports custom BCC, replyTO, from, subject, etc.

What you'll probably use most of is the replacements. You can set tokens in your mailer template (MadMimi calls these "promotions") that look like this:

	Hi there! This is a token: {{token}}

To tell MadMimi to replace that token with a given value - just send that replacement in:
    
	var result = mimi.SendMailer("MyPromo", "rob@wekeroad.com", replacements: new {token = "STEVIE NICKS IS AWESOME"});

Some mailers need multiple replacements. Can you guess how to do that? 