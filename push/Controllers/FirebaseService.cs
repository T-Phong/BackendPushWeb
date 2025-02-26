using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FirebaseService
{
    public FirebaseService()
    {
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("firebase-adminsdk.json")
        });
    }

    public async Task<string> SendNotification(string token, string title, string body)
    {
        var message = new Message()
        {
            Token = token,
            Notification = new Notification()
            {
                Title = title,
                Body = body
            }
        };

        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        return response;
    }
}
