using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class KSPmain : MonoBehaviour {

    public Camera _camera;
    public InputField _sanku;
    public InputField _best;
    public InputField _hansyoku;
    public InputField _tane;
    public Text KSP;
    public Text sp_come;
    public Text ksp_come;
    public Dropdown seityo_;
    public Dropdown seibetu_;
    public Dropdown spkome_;
    public Dropdown nyukyu_age_;
    public Dropdown nyukyu_month_;
    public Dropdown nyukyu_week_;

    private double seityo;
    private double seibetu;
    private double spkome;
    private double nyukyu_age, nyukyu_month, nyukyu_week;

    public void hantei()
    { 
        /*---Textをint型に変換---*/
        string sanku_, best_, hansyoku_, tane_;
        sanku_ = _sanku.text;
        best_ = _best.text;
        hansyoku_ = _hansyoku.text;
        tane_ = _tane.text;

        double sanku = double.Parse(sanku_);
        double best = double.Parse(best_);
        double hansyoku = double.Parse(hansyoku_);
        double tane = double.Parse(tane_);

        /*---KSP計算---*/
        double Pa1 = (sanku * 4.4) / (best - 256 ) + 16;
        double Pa2 = (sanku + 100) * 4.4 / (best - 256) + 16;
        double P0  = (hansyoku / 400) + (tane * 6 / 1000) + seibetu;

        double S1, S2;

        if(Pa1 > 80)
        {
            S1 = (Pa1 - 80) * 4 + (80 - P0) * 2;
        }
        else
        {
            S1 = (Pa1 - P0) * 2;
        }
        if (Pa2 > 80)
        {
            S2 = (Pa2 - 80) * 4 + (80 - P0) * 2;
        }
        else
        {
            S2 = (Pa2 - P0) * 2;
        }

        double SP1 = 0, SP2 = 0;
        double _SP1 = 0, _SP2 = 0;
        double nyukyu = nyukyu_month * 4 + nyukyu_week;

        /////　超早熟　早熟　普通型　/////
        if (seityo >= 0.6)
        {
            SP1 = S1 / (seityo + 0.1) + 1;
            SP2 = S2 / seityo;
            _SP1 = Math.Truncate(SP1);
            _SP2 = Math.Truncate(SP2);

        }
        else if (seityo_ID == 1)
            {
                SP1 = S1 / (0.5 + 0.1);
                SP2 = S2 / 0.5;
                _SP1 = Math.Truncate(SP1);
                _SP2 = Math.Truncate(SP2);

            }
            /////　遅普通　晩成　超晩成型　/////
        else
        {
            SP1 = (S1 + nyukyu) * 2 + 1;
            SP2 = (S2 + nyukyu) * 2 + 1;
            _SP1 = Math.Truncate(SP1);
            _SP2 = Math.Truncate(SP2);
        }
        /*---KSP数値測定---*/
        int SP1_ = (int)_SP1;
        int[] KSP_ = new int[3];
        int j = 0;

        for( int i = SP1_ ; i <= _SP2; i++)
        {
            if(i % 8 == spkome)
            {
                KSP_[j] = i -35;
                j++;
            }
            if (j >= 3)
            {
                break;
            }
        }
        if (KSP_[1] == 0)
        {
            KSP.text = KSP_[0].ToString();
        }
        else if (KSP_[2] == 0)
        {
            KSP.text = KSP_[0].ToString() + "  " + KSP_[1].ToString();
        }
        else {
            KSP.text = KSP_[0].ToString() + "  " + KSP_[1].ToString() + "  " + KSP_[2].ToString();
        }
        sp_come.text = _SP1.ToString() +"  ≦  SP  ≦  " + _SP2.ToString();
        int _ksp1 = (int)_SP1 - 35, _ksp2 = (int)_SP2 - 35;
        ksp_come.text = _ksp1.ToString() + "  ≦  KSP  ≦  " + _ksp2.ToString();

    }

        /// 以下数値代入ドロップダウンリスト用

    /// 成長型 ///

    private int seityo_ID; 

    public void seityo_0(int result)
    {
        switch (result)
        {
            case 0:
                seityo = 0.7;
                seityo_ID = 0;
                break;
            case 1:
                seityo = 0.6;
                seityo_ID = 0;
                break;
            case 2:
                seityo = 0.5;
                seityo_ID = 1;
                break;
            default:
                seityo = 0.5;
                seityo_ID = 0;
                break;
        }
    }

    /// 性別 ///
    public void seibetu_0(int result)
    {
        switch (result)
        {
            case 0:
                seibetu = 10.5;
                break;
            case 1:
                seibetu = 8.5;
                break;
            default:
                break;
        }
    }

    /// SPコメント週（奇数月1週目から） ///
    public void spkome_0(int result)
    {
        switch (result)
        {
            case 0:
                spkome = 1;
                break;
            case 1:
                spkome = 0;
                break;
            case 2:
                spkome = 3;
                break;
            case 3:
                spkome = 2;
                break;
            case 5:
                spkome = 5;
                break;
            case 6:
                spkome = 4;
                break;
            case 7:
                spkome = 7;
                break;
            case 8:
                spkome = 6;
                break;
            default:
                break;
        }
    }
    public void nyukyu_month_0(int result)
    {
        nyukyu_month = result;
    }
    public void nyukyu_week_0(int result)
    {
        nyukyu_week = result;
    }

    /////  背景色・文字色変更　/////

    public Text sanku_tx, best_tx, hinba_tx, tane_tx, seityo_tx, seibetu_tx,
                spkome_tx, nyukyu_tx, KSP_tx, KSP_re_tx, SP_re_tx, KSP_res_tx,
                button_text;
    int change_color_ID = 0;

    public void change_color()
    {
        if (change_color_ID == 0)
        {
            sanku_tx.color = Color.white;
            best_tx.color = Color.white;
            hinba_tx.color = Color.white;
            tane_tx.color = Color.white;
            seityo_tx.color = Color.white;
            seibetu_tx.color = Color.white;
            spkome_tx.color = Color.white;
            nyukyu_tx.color = Color.white;
            KSP_tx.color = Color.white;
            KSP_re_tx.color = Color.cyan;
            SP_re_tx.color = Color.white;
            KSP_res_tx.color = Color.white;
            _camera.backgroundColor = Color.black;
            button_text.text = "白背景";
            change_color_ID = 1;
        }
        else
        {
            sanku_tx.color = Color.black;
            best_tx.color = Color.black;
            hinba_tx.color = Color.black;
            tane_tx.color = Color.black;
            seityo_tx.color = Color.black;
            seibetu_tx.color = Color.black;
            spkome_tx.color = Color.black;
            nyukyu_tx.color = Color.black;
            KSP_tx.color = Color.black;
            KSP_re_tx.color = Color.red;
            SP_re_tx.color = Color.black;
            KSP_res_tx.color = Color.black;
            _camera.backgroundColor = Color.white;
            button_text.text = "黒背景";

            change_color_ID = 0;
        }
    }
 
    // Use this for initialization
    void Start () {
        seityo = 0.7;
        seityo_ID = 0;
        seibetu = 10.5;
        spkome = 1;
    }


    // Update is called once per frame
    void Update() { 

    }
}
