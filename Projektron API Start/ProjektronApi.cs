using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1 {
  class ProjektronApi : IProjektronApi {
    private readonly string _url;
    private readonly bool _secure;
    private readonly WebClient _client;
    private bool _gotSessionTicket;
    private string _sessionCookie;
    const string LoginUrl = "login";

    private const string LastNameGroupName = "LastName";
    private const string FirstNameGroupName = "FirstName";
    private const string InstanceNameGroupName = "InstanceName";
    private readonly Regex _userNameRegex = new Regex($@"\<title\>BCS\: \.\.\. \/\/ (?<{ProjektronApi.LastNameGroupName}>[\w\d]*), (?<{ProjektronApi.FirstNameGroupName}>[\w\d]*) \[(?<{ProjektronApi.InstanceNameGroupName}>[\w\d]*)\]</title>", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    private KeyValuePair<string, string> Cookie { get { return new KeyValuePair<string, string>("COOKIE", this._sessionCookie); } }

    public string InstanceName { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public bool Logedin { get; private set; }

    public ProjektronApi(string url, bool secure) {
      this._client = new WebClient();
      this._url = url;
      this._secure = secure;
      this._gotSessionTicket = false;
    }
    private string GetTextFromResponse(HttpWebResponse response) {
      using (Stream s = response.GetResponseStream()) {
        using (StreamReader sr = new StreamReader(s)) {
          return sr.ReadToEnd();
        }
      }
    }

    private void Initialize(HttpWebResponse response) {
      string responseText = GetTextFromResponse(response);
      Match x = _userNameRegex.Match(responseText);
      this.LastName = x.Groups[LastNameGroupName].Value;
      this.FirstName = x.Groups[FirstNameGroupName].Value;
      this.InstanceName = x.Groups[InstanceNameGroupName].Value;
    }


    private string GetBaseUrl(bool includeBcs = true) {
      string s = this._secure ? "s" : "";
      string bcs = includeBcs ? "/bcs" : "";
      return $"http{s}://{this._url}{bcs}/";
    }

    private IDictionary<string, string> GetDefaultHeaders() {
      IDictionary<string, string> headers = new Dictionary<string, string>();

      headers.Add(this.Cookie);
      headers.Add("Upgrade-Insecure-Requests", "1");
      headers.Add("DNT", "1");

      return headers;
    }

    private IDictionary<string, string> GetLoginParameters(string userName, SecureString password) {
      IDictionary<string, string> parameters = new Dictionary<string, string>();
      parameters.Add("login", "Anmelden");
      parameters.Add("isPassword", "pwd");
      parameters.Add("user", userName);
      parameters.Add("pwd", password.ToStandartString());
      return parameters;
    }

    /// <summary>
    /// This method is only used to call the login page
    /// during this process a cookie is generated
    /// this cookie is used to put it in the header of every 
    /// </summary>
    public void EnsureSessionTicket() {
      Func<Cookie, string> ac = cookie => $"{cookie.Name}={cookie.Value}";

      if (!this._gotSessionTicket) {
        string url = this.GetBaseUrl() + LoginUrl;
        HttpWebResponse response = this._client.Get(url);
        if (response.Cookies.Count == 1) {
          this._gotSessionTicket = true;
          foreach (Cookie cookie in response.Cookies) {
            this._sessionCookie = ac(cookie);
          }
        } else {
          //TODO exception type
          throw new Exception("Invalid Cookies Count");
        }
      }
    }

    public bool Login(string userName, SecureString password) {
      // todo get this from the form of the "EnsureSessionTicket" method?
      const string formNamePageform = @"/*/display?compose_login_form_only=false&login_form_name=pageform";
      const string loginAccept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
      const string acceptEncoding = "deflate";
      const string contentType = "application/x-www-form-urlencoded";

      this.EnsureSessionTicket();
      string baseUrl = this.GetBaseUrl();
      string url = baseUrl + LoginUrl + formNamePageform;

      IDictionary<string, string> headers = GetDefaultHeaders();
      IDictionary<string, string> parameters = GetLoginParameters(userName, password);

      PostParameters postParameters = new PostParameters(url, headers, parameters, loginAccept, acceptEncoding, contentType);

      HttpWebResponse response = this._client.Post(postParameters);
      Initialize(response);
      //todo validation (for now this will do since you will get an exception, 401 i guess, if the login attempt failes)

      this.Logedin = true;
      return this.Logedin;
    }

    public void GetApointments() {

    }

    public void DayEffortRecording() {
      HttpWebResponse response = this._client.Get("http://172.16.1.40/bcs/mybcs/dayeffortrecording/display?oid=1464165990785_JUser");
      string text = GetTextFromResponse(response);
    }

    public void Presence() {
      string url = "http://172.16.1.40/bcs/workspacecontent/*/display?pageinframe=workspaces&transactionId=1477070614094-5473110096251278";
      HttpWebResponse response = this._client.Get(url);
      string text = GetTextFromResponse(response);

    }
  }
}