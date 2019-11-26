namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs
{
    using HtmlAgilityPack;

    public static class TeamsProviderStub
    {
        public static HtmlNode GetValidMatchContainerWithTeamsWithoutScores()
        {
            string html = @"
                <div>
                    <div class='__app-PromoMatchBody-competitor-name promoMatchBody__competitor-name___3IM6A'>Antrophy</div>
                    <div class='__app-PromoMatchBody-competitor-name promoMatchBody__competitor-name___3IM6A'>The Prime</div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetValidMatchContainerWithTeamsAndScores()
        {
            string html = @"
                <div>
                    <div class='__app-PromoMatchBody-competitor-name promoMatchBody__competitor-name___3IM6A'>Antrophy</div>
                    <div class='__app-PromoMatchBody-competitor-name promoMatchBody__competitor-name___3IM6A'>The Prime</div>
                    <div class='promoMatchBody__score-point___12e83'>2</div>
                    <div class='promoMatchBody__score-point___12e83'>5</div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetInvalidMatchContainerWithMissingChildNode()
        {
            string html = @"<div class='__app-PromoMatchBody-competitor-name promoMatchBody__competitor-name___3IM6A'></div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetInvalidMatchContainerWithMissingTeamNames()
        {
            string html = @"
                <div>
                    <div class='__app-PromoMatchBody-competitor-name promoMatchBody__competitor-name___3IM6A'></div>
                    <div class='__app-PromoMatchBody-competitor-name promoMatchBody__competitor-name___3IM6A'></div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }

        public static HtmlNode GetInvalidMatchContainerWithOnlyOneTeam()
        {
            string html = @"
                <div>
                    <div class='__app-PromoMatchBody-competitor-name promoMatchBody__competitor-name___3IM6A'>Antrophy</div>
                </div>";

            return HtmlNodesLoader.Load(html);
        }
    }
}