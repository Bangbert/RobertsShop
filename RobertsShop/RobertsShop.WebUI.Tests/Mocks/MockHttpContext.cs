using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RobertsShop.WebUI.Tests.Mocks
{
    public  class MockHttpContext: HttpContextBase
    {
        private MockRequest request;
        private MockResponse response;
        private HttpCookieCollection cookies;

        public MockHttpContext()
        {
            cookies = new HttpCookieCollection();
            response = new MockResponse(cookies);
            request = new MockRequest(cookies);

        }
        public override HttpRequestBase Request
        {
            get { return request; }
        }
        public override HttpResponseBase Response
        {
            get
            {
                return response;
            }

        }

    }
    public class MockResponse : HttpResponseBase
    {
        private readonly HttpCookieCollection cookies;

        public MockResponse(HttpCookieCollection _cookies)
        {
            cookies = _cookies;

        }

        public override HttpCookieCollection Cookies {
            get
            {
                return cookies;
            }
        }
    }
    public class MockRequest : HttpRequestBase
    {
        private readonly HttpCookieCollection cookies;

        public MockRequest(HttpCookieCollection _cookies)
        {
            cookies = _cookies;

        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return cookies;
            }
        }
    }

}
