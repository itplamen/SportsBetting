namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs
{
    public static class HtmlServiceStub
    {
        public static string GetEventWithOneUrl()
        {
            string html = @"
                <div>
                    <div class='sportEventRow__body___3Ywcg'>
                        <a href='/en/betting/match/5:ce15520c-4003-49c7-aad6-4c1c102f6a85'></a>
                    </div>
                </div>";

            return html;
        }
    }
}
