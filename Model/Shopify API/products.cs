using minor.Shopify.Get;

public class products{
    public double id{get;set;}
    public string title{get;set;}
    public string body_html{get;set;}
    public string vendor{get;set;}
    public string product_type{get;set;}
    public DateTime created_at{get;set;}
    public string handle{get;set;}
    public DateTime updated_at{get;set;}
    public DateTime published_at{get;set;}
    public string template_suffix{get;set;}
    public string published_scope{get;set;}
    public string tags{get;set;}
    public string status{get;set;}
    public string admin_graphql_api_id{get;set;}
    public List<variants> variants{get;set;}
    public List<options> options{get;set;}
    public List<images> images{get;set;}

}