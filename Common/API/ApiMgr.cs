/*using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.Cookies;
using System;
using BestHTTP.JSON.LitJson;

/// <summary>
/// ����BestHttp�Ľӿڵ��ù�����
/// </summary>
public class ApiMgr : BaseInstance_Cla<ApiMgr_Cla>
{
    private Action<string> _pri_APIAction;//�ӿڻص����ݵ��¼�
    public List<Cookie> _Pub_Cookies;//��¼���ȡ��cookies
    public string _Pub_Result;//���ؽ��

    #region �ӿڵ���
    /// <summary>
    /// ���¼��ص��Ľӿڵ���Э��
    /// </summary>
    /// <param name="url">�ӿڵ�ַ</param>
    /// <param name="dataDic">���ݲ���</param>
    /// <param name="action">�ص��¼�</param>
    /// <returns></returns>
    public IEnumerator UseAPI_IEnu(string url, Dictionary<string, object> dataDic, Action<string> action)
    {
        using (HTTPRequest request = new HTTPRequest(new Uri(Constants.URL_CONSTANTS_LOCALHOST + url), HTTPMethods.Post, OnRequestFinished))
        {
            //��Ӳ���
            if (dataDic != null && dataDic.Count > 0)
            {
                foreach (var data in dataDic)
                {
                    request.AddField(data.Key, data.Value.ToString());
                }
            }
            //���Cookies
            request.Cookies = _Pub_Cookies;
            //�����¼�
            _pri_APIAction = action;
            //��������
            yield return request.Send();
        }
    }
    /// <summary>
    /// ����ӿڻص�
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        //�����ӿڷ��ص����ݣ����ݽӿ��޸ģ�Ĭ�Ϸ���ȫ������
        //_Pub_Result = GetReturnData(response.DataAsText);
        //�ص�ִ���¼������ӿڷ��ز���
        _pri_APIAction?.Invoke(response.DataAsText);
    }
    //����json�е�����(���Ը����Լ���̨�ӿڵķ���ֵ������д)
    public virtual string GetReturnData(string jsonString)
    {
        JsonData jsonData = JsonMapper.ToObject(jsonString);
        JsonData jsonReturnData = jsonData["data"];
        JsonData jsonReturnCode = jsonData["code"];
        //�ӿڷ���codeΪ200ʱ�ŷ���data�е�����
        return jsonReturnCode.ToString().Equals("200") ? JsonMapper.ToJson(jsonReturnData) : null;
    }
    #endregion
}
*/