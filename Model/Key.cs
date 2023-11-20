using System.ComponentModel.DataAnnotations;
using NuGet.Common;

public class APIKey{

    [Key]
    public string Key {get;set;}
    public string Brand{get;set;}
    public string Email{get;set;}
    public DateTime signUpDate{get;set;}
    public DateTime NextBillingDue{get;set;}
}