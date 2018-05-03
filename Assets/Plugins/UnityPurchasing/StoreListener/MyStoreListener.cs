//using System;
//using UnityEngine;
//using UnityEngine.Purchasing;
//
//public class MyStoreListener : MonoBehaviour, IStoreListener
//{
//	void Awake()
//	{
//		//ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//		//UnityPurchasing.Initialize(this, builder);
//	}
//
//	public void OnInitializeFailed(InitializationFailureReason error) {
//		
//	}
//
//	public void OnPurchaseFailed(Product item, PurchaseFailureReason r) {
//		
//	}
//		
//	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e) {
//		Debug.Log ("item comprado: "+e.purchasedProduct.definition.id);
//		return PurchaseProcessingResult.Complete;
//	}
//
//	#if UNITY_IOS || UNITY_STANDALONE_OSX
//	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//	{
//		extensions.GetExtension<IAppleExtensions> ().RestoreTransactions (result => {
//			if (result) {
//				// This does not mean anything was restored,
//				// merely that the restoration process succeeded.
//				ProcessPurchase(
//			} else {
//				// Restoration failed.
//			}
//		};
//					}
//	#endif
//}