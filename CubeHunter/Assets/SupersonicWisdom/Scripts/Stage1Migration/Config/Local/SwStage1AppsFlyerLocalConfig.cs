#if SW_STAGE_STAGE1_OR_ABOVE
using System.Collections.Generic;

namespace SupersonicWisdomSDK
{
    internal class SwStage1AppsFlyerLocalConfig : SwLocalConfig
    {
        #region --- Constants ---

        public const string APPS_FLYER_DEFAULT_DOMAIN_DEFAULT_VALUE = "appsflyersdk.com";
        private const string APPS_FLYER_DEFAULT_DOMAIN_KEY = "appsFlyerDomain";

        #endregion


        #region --- Properties ---

        public override Dictionary<string, object> LocalConfigValues
        {
            get
            {
                return new Dictionary<string, object>
                {
                    { APPS_FLYER_DEFAULT_DOMAIN_KEY, APPS_FLYER_DEFAULT_DOMAIN_DEFAULT_VALUE }
                };
            }
        }

        #endregion
    }
}
#endif