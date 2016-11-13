using UnityEngine;
using System.Collections;
using System;

public class DayNightCycle : MonoBehaviour {
	public float exposure=1f;

	public float time;

	public TimeSpan currentTime;
	public Transform sunTransform;
	public Light sunLight;

	public int days;
	public float intensity;

	public Color fogDay=Color.grey;
	public Color fogNight=Color.blue;

	public int speed;

    //day/night audio
    public AudioClip nightClip;
    AudioSource audiosource;




	// Use this for initialization
	void Start () {

        audiosource = this.GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {






        ChangeTime();

	
	}

	public void ChangeTime()
	{
		

		time +=Time.deltaTime *speed;

		if(time >86400)
		{
			days +=1;
			time=0;
		}

		currentTime=TimeSpan.FromSeconds(time);

		sunTransform.rotation=Quaternion.Euler(new Vector3((time-21600)/86400*360,0,0));

		if(time<43200)
		{
			intensity=1- (43200-time) /43200;

		}
		else
			intensity=1- ((43200-time) /43200*-1);
		

		RenderSettings.fogColor=Color.Lerp(fogDay,fogNight,intensity*intensity);

		sunLight.intensity=intensity;

		if(time>43200)
		{
			exposure =1f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}
			
		 if(time>1000)
		{
			exposure =0.1f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}
		if(time>10000)
		{
			exposure =0.25f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}
		if(time>20000)
		{
			exposure =0.45f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}

		if(time>25000)
		{
			exposure =0.6f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}
		if(time>30000)
		{
			exposure =0.75f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}


	

		if(time>40000)
		{
			exposure =01f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}
		if(time>50000)
		{
			exposure =0.8f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}
		if(time>60000)
		{
			exposure =0.65f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}
		if(time>70000)
		{
			exposure =0.5f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}

		if(time>80000)
		{
			exposure =0.3f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
            audiosource.clip = nightClip;
            audiosource.Play();
		}

		if(time>83000)
		{
			exposure =0.2f;
			RenderSettings.skybox.SetFloat("_Exposure", exposure);
		}
	

	}
}
