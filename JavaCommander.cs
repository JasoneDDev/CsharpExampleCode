using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JavaCommander : MonoBehaviour,  
	OuyaSDK.IPauseListener,
    OuyaSDK.IResumeListener,
    OuyaSDK.IMenuButtonUpListener,
    OuyaSDK.IMenuAppearingListener,
   OuyaSDK.IGetProductsListener, OuyaSDK.IPurchaseListener, OuyaSDK.IGetReceiptsListener
{
    
   	GUI_HUD gui;
    loadingScript loader;
    
   	void Start()
	{
		gui = FindObjectOfType(typeof(GUI_HUD)) as GUI_HUD;
		loader = GetComponent(typeof(loadingScript)) as loadingScript;
	}
	
	void Awake()
    {
        OuyaSDK.registerMenuButtonUpListener(this);
        OuyaSDK.registerMenuAppearingListener(this);
        OuyaSDK.registerPauseListener(this);
        OuyaSDK.registerResumeListener(this);
        OuyaSDK.registerGetProductsListener(this);
        OuyaSDK.registerPurchaseListener(this);
        OuyaSDK.registerGetReceiptsListener(this);
        loader = this.GetComponent<loadingScript>();
    }

    void OnDestroy()
    {
        OuyaSDK.unregisterMenuButtonUpListener(this);
        OuyaSDK.unregisterMenuAppearingListener(this);
        OuyaSDK.unregisterPauseListener(this);
        OuyaSDK.unregisterResumeListener(this);
        OuyaSDK.unregisterGetProductsListener(this);
        OuyaSDK.unregisterPurchaseListener(this);
        OuyaSDK.unregisterGetReceiptsListener(this);
    }

    public void OuyaMenuButtonUp()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
        Debug.Log("HERE I AM FINALLY WORKING");
        gui = FindObjectOfType (typeof(GUI_HUD)) as GUI_HUD;
       
        
        if(gui)
        {
        	//gui.PauseMenu();
        }
       
    }

    public void OuyaMenuAppearing()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

    public void OuyaOnPause()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

    public void OuyaOnResume()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }
    
    public void OuyaGetProductsOnSuccess(List<OuyaSDK.Product> products )
    {
        m_products.Clear();
        foreach (var product  in products)
        {
            m_products.Add(product);
        }
        GetReceipts();
        Debug.Log("get receipts");
    }

    public void OuyaGetProductsOnFailure( int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("OuyaGetProductsOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
        gui = FindObjectOfType (typeof(GUI_HUD)) as GUI_HUD;
        if(gui!=null)
        {
        gui.Error();
        }
    }

    public void OuyaGetProductsOnCancel()
    {
        Debug.LogError("OuyaGetProductsOnCancel:");
    }

    public void OuyaPurchaseOnSuccess(OuyaSDK.Product product )
    {
        // bought it now setting to bought
        
        PlayerPrefs.SetInt("StartCount",10);
        PlayerPrefs.SetString("WW3DPurchased","ofCourseIBoughtIt");
        PlayerPrefs.SetString("NeverLoaded","IHaveBeenLoaded");
        loader.WW3DIsUnlocked = true;
        loader.gameIsUnlocked = true;
        PlayerPrefs.Save();
        Application.LoadLevel(Application.loadedLevelName);
        //-----------------------------------
    }

    public void OuyaPurchaseOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("OuyaPurchaseOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
        gui = FindObjectOfType (typeof(GUI_HUD)) as GUI_HUD;
        if(gui!=null)
        {
        gui.Error();
        }
    }

    public void OuyaPurchaseOnCancel()
    {
        Debug.LogError("OuyaPurchaseOnCancel:");
    }

    public void OuyaGetReceiptsOnSuccess(List<OuyaSDK.Receipt> receipts )
    {
        m_receipts.Clear();
       Debug.Log("Got receipts");
       var purchased=false;
         foreach (var receipt in receipts)
        {
            m_receipts.Add(receipt);
            if(receipt.getIdentifier() == m_products[0].getIdentifier() || receipt.getIdentifier() == m_products[1].getIdentifier())
            {
            //already purchased
            purchased=true;
            PlayerPrefs.SetInt("StartCount",10);
            PlayerPrefs.SetString("WW3DPurchased","ofCourseIBoughtIt");
        	PlayerPrefs.SetString("NeverLoaded","IHaveBeenLoaded");
        	loader.WW3DIsUnlocked = true;
        	 loader.gameIsUnlocked = true;
        	PlayerPrefs.Save();
        	Application.LoadLevel(Application.loadedLevelName);
            //CREDIT PURCHASE HERE AND SET TO BOUGHT
            
            }
        }
        
        if(!purchased && PlayerPrefs.GetInt("StartCount") <2 )
        {
        	PurchaseDiscount();
        }
        else if(!purchased)
        {
        	Purchase();
        }
        
    }

    public void OuyaGetReceiptsOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("OuyaGetReceiptsOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
            gui = FindObjectOfType (typeof(GUI_HUD)) as GUI_HUD;
        if(gui!=null)
        {
        gui.Error();
        }
    }

    public void OuyaGetReceiptsOnCancel()
    {
        Debug.LogError("OuyaGetReceiptsOnCancel:");
    }

  //  #region Data containers

     List<OuyaSDK.Product> m_products  = new List<OuyaSDK.Product>();

     List<OuyaSDK.Receipt> m_receipts  = new List<OuyaSDK.Receipt>();

  //  #endregion
  
  void GetProducts()
  {
  Debug.Log("getting products");
  	List<OuyaSDK.Purchasable> productIdentifierList   = new List<OuyaSDK.Purchasable>();

               foreach (var productId in OuyaGameObject.Singleton.Purchasables)
                {
                    productIdentifierList.Add(new OuyaSDK.Purchasable(productId));
                }
Debug.Log("getting productsvvvv");
                OuyaSDK.requestProductList(productIdentifierList);
  }
  
  void GetDProducts()
  {
  Debug.Log("getting products");
  	List<OuyaSDK.Purchasable> productIdentifierList   = new List<OuyaSDK.Purchasable>();

                foreach (var productId in OuyaGameObject.Singleton.Purchasables)
                {
                    productIdentifierList.Add(new OuyaSDK.Purchasable(productId));
                }
Debug.Log("getting productsvvvv");
                OuyaSDK.requestProductList(productIdentifierList);
  }
  
  void Purchase()
  {
  	Debug.Log("buying stuff");
  	OuyaSDK.requestPurchase(m_products[0].getIdentifier());
  }
  
  void PurchaseDiscount()
  {
  	Debug.Log("buying discounted stuff");
  	OuyaSDK.requestPurchase(m_products[1].getIdentifier());
  }
  
  void GetReceipts()
  {
  	OuyaSDK.requestReceiptList();
  	Debug.Log("getting receipts");
  }
  
}
