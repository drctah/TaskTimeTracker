using System.Net;
using System.Security;

namespace ConsoleApplication1 {
  internal interface IRequestBuilder {
    RequestBuilder ContentType(string contentType);
    RequestBuilder KeepAlive();
    RequestBuilder RefererIs(string referer);
    RequestBuilder TimeoutIs(int timeout);
    RequestBuilder UserAgentIs(string userAgent);
    RequestBuilder WithCookieContainer(CookieContainer cookieJar);
    RequestBuilder Method(string metod);
    RequestBuilder WithPostData(byte[] postData);
    RequestBuilder AddPostParameter(string parameterName, string content);
    RequestBuilder AddPostParameter(string parameterName, SecureString content);
    HttpWebRequest GetRequest();
  }
}