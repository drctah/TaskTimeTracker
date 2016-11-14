using System.Collections.Generic;

namespace ConsoleApplication1 {
  class PostParameters {
    public string Url { get; }
    public IDictionary<string, string> Headers { get; }
    public IDictionary<string, string> Parameters { get; }
    public string Accept { get; }
    public string AcceptEncoding { get; }
    public string ContentType { get; }

    public PostParameters(string url, IDictionary<string, string> headers, IDictionary<string, string> parameters, string accept, string acceptEncoding, string contentType) {
      this.Url = url;
      this.Headers = headers;
      this.Parameters = parameters;
      this.Accept = accept;
      this.AcceptEncoding = acceptEncoding;
      this.ContentType = contentType;
    }
  }
}