using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cf.Googles.AdMobs
{
    public class AdMobManager : MonoBehaviour
    {
        [Title("")]
        [ShowInInspector] [ReadOnly] private InterstitialAd Ad { get; set; }
        
        private void Awake()
        {
            MobileAds.Initialize(_ =>
            {
                
            });
        }

        private void Start()
        {
            Load(AdMobType.Full);
        }

        private void Load(AdMobType type)
        {
            // 생성
            var adRequest = new AdRequest();

            // 기존 광고 유무 확인
            if (Ad != null)
            {
                Ad.Destroy();
                Ad = null;
            }
            
            // 아이디 획득
            var adId = LoadId(type);

            // 광고 로드
            InterstitialAd.Load(adId, adRequest, LoadCallback);
        }

        private string LoadId(AdMobType type) => type switch
        {
            AdMobType.Full => "ca-app-pub-3463254774821238/3410434492",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        private void LoadCallback(InterstitialAd ad, LoadAdError error)
        {
            if (error != null || ad == null)
            {
                Debug.LogError($"Google AdMob Init Failed");
                
                return;
            }

            Ad = ad;
            
            RegisterEventHandler(Ad);
        }

        #region > Event

        private void RegisterEventHandler(InterstitialAd ad)
        {
            // 광고 지급
            ad.OnAdPaid += OnEventOnAdPaid;   
            // ???
            ad.OnAdImpressionRecorded += OnEventOnOnAdImpressionRecorded; 
            // 광고 클릭
            ad.OnAdClicked += OnEventOnAdClicked;  
            // 전면 광고 열림
            ad.OnAdFullScreenContentOpened += OnEventFullScreenContentOpened;
            // 전면 광고 닫힘
            ad.OnAdFullScreenContentClosed += OnEventFullScreenContentClosed;
            // 전면 광고 호출 시류ㅐ
            ad.OnAdFullScreenContentFailed += OnEventFullScreenContentFailed;
        }

        private void OnEventOnAdPaid(AdValue adValue)
        {
            
        }

        private void OnEventOnOnAdImpressionRecorded()
        {
            
        }

        
        private void OnEventOnAdClicked()
        {
            
        }

        
        private void OnEventFullScreenContentOpened()
        {
            
        }

        
        private void OnEventFullScreenContentClosed()
        {
            
        }

        
        private void OnEventFullScreenContentFailed(AdError error)
        {
            
        }

        #endregion

    }
}
