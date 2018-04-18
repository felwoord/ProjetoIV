using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ShowAds : MonoBehaviour
{
    //Colocar aqui um panel com tela cheia
    //contendo um botao para chamar OpenMyGame
    public GameObject minhaPropaganda;

    //https://unityads.unity3d.com/help/monetization/integration-guide-unity
    void Start()
    {
    }

    //Interstitial = propaganda de tela inteira
    //onde o jogador pode pular sem assistir inteiro
    //parecida com as do YouTube
    public void ShowInterstitial()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
        else
        {
            minhaPropaganda.SetActive(true);
        }
    }

    //Rewarded = propaganda de tela cheia onde o jogador nao pode pular
    //normalmente essa propaganda é utilizada junto com uma recompensa
    public void ShowRewarded()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            minhaPropaganda.SetActive(true);

        }
    }

    //funcao para verificar se o video Rewarded foi assistido por completo
    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Assistiu o video de recompensa completo! Parabens!");
            // dar um premio para o jogador

        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Pulo o video e nao assistiu inteiro! Shame on you!");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Falha ao carregar o video.");
        }
    }

    //abre o jogo direto na loja
    public void OpenMyGame()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.thetisgames.googleplay.flywings2017flightsimulator.free&hl=pt_BR");
#elif UNITY_IOS
        Application.OpenURL("https://itunes.apple.com/app/flight-simulator-flywings-2017/id1187376935?mt=8");
#endif
    }
}
