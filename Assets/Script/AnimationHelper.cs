using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : Singleton<AnimationHelper> {

    public Dictionary<GameObject, Coroutine> animatedGOs = new Dictionary<GameObject, Coroutine>();

    #region SCALE
    public Coroutine Scale(GameObject GO, Vector3 targetScale, float time)
    {
        if (animatedGOs.ContainsKey(GO) == true)
            return null;

        Coroutine cc = this.StartCoroutine(ScaleCoroutine(GO, targetScale, time));

        animatedGOs.Add(GO, cc);

        return cc;
    }
    IEnumerator ScaleCoroutine(GameObject GO, Vector3 targetScale, float time)
    {
        float currTime = 0;
        Vector3 initialScale = GO.transform.localScale;
        Vector3 currScale = Vector3.Lerp(GO.transform.localScale, targetScale, currTime);
        while (currScale.x != targetScale.x || currScale.x != targetScale.y || currScale.z != targetScale.z)
        {
            if (GO == null)
                break;

            currTime += (Time.deltaTime / time);
            currScale = Vector3.Lerp(initialScale, targetScale, currTime);
            GO.transform.localScale = currScale;
            yield return null;
        }

        animatedGOs.Remove(GO);
    }
    #endregion


    #region FADE
    /**
     *      --- FADE ---
     */
    public void FadeOut(GameObject GO, float time, bool shouldDestroyAfterFadeout = false)
    {
        if (animatedGOs.ContainsKey(GO) == true)
            return;
        if (GO == null)
            return;

        Renderer[] rendererComponents = GO.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in rendererComponents)
        {
            ColorRenderer colorRenderer = new ColorRenderer();
            if (colorRenderer.SetRenderer(renderer) == true)
            {
                colorRenderer.SetColor(new Color(colorRenderer.GetColor().r, colorRenderer.GetColor().g, colorRenderer.GetColor().b, 1f));
            }
        }

        animatedGOs.Add(GO, this.StartCoroutine(FadeOutCoroutine(GO,time, shouldDestroyAfterFadeout)));
    }
    IEnumerator FadeOutCoroutine(GameObject GO, float time,bool shouldDestroy = false)
    {
        // store here all renderers with their initial colors of the object and its children
        Dictionary<ColorRenderer,float> renderers = new Dictionary<ColorRenderer, float>();
        
        // add all renderers of the object and its children
        Renderer[] rendererComponents = GO.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in rendererComponents)
        {
            ColorRenderer colorRenderer = new ColorRenderer();
            if (colorRenderer.SetRenderer(renderer) == true)
            {
                renderers.Add(colorRenderer,colorRenderer.GetColor().a);
            }
        }
        
        // animated all renderers from 
        for (float currTime = time; currTime >= 0; currTime -= Time.deltaTime)
        {
            foreach (ColorRenderer cr in renderers.Keys){
                Color c = cr.GetColor();
                c.a = ConvertRangeClamped(0, time, 0, renderers[cr], currTime);
                cr.SetColor(c);
            }
            yield return null;
        }

        // fade out 100%
        foreach (ColorRenderer cr in renderers.Keys)
        {
            Color c = cr.GetColor();
            c.a = 0;
            cr.SetColor(c);
        }

        animatedGOs.Remove(GO);
        if (shouldDestroy == true)
        {
            Destroy(GO);
        }
    }
    #endregion



    #region BLINK
    /**
     *      --- BLINK ---
     */
    public void Blink(GameObject GO, Color targetColor, float time)
    {
        if (animatedGOs.ContainsKey(GO) == true)
            return;

        animatedGOs.Add(GO, this.StartCoroutine(BlinkCoroutine(GO, targetColor, time)));
    }
    IEnumerator BlinkCoroutine(GameObject GO, Color targetColor, float time)
    {
        Color targetColorCopy = new Color(targetColor.r, targetColor.g, targetColor.b, targetColor.a);

        // store here all renderers with their initial colors of the object and its children
        Dictionary<ColorRenderer, Color> renderers = new Dictionary<ColorRenderer, Color>();


        // add all renderers of the object and its children
        Renderer[] rendererComponents = GO.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in rendererComponents)
        {
            ColorRenderer colorRenderer = new ColorRenderer();
            if (colorRenderer.SetRenderer(renderer) == true)
            {
                renderers.Add(colorRenderer, colorRenderer.GetColor());
                colorRenderer.SetColor(targetColorCopy);
            }
        }

        // animated all renderers from 
        for (float currTime = time; currTime >= 0; currTime -= Time.deltaTime)
        {
            float normalizedTime = ConvertRangeClamped(0, time, 0, 1, currTime);

            foreach (ColorRenderer cr in renderers.Keys)
            {
                Color origColor = renderers[cr];
                cr.SetColor(Color.Lerp(origColor,targetColor,normalizedTime));
            }
            yield return null;
        }

        // restore to original color 100%
        foreach (ColorRenderer cr in renderers.Keys)
        {
            Color origColor = renderers[cr];
            cr.SetColor(origColor);
        }

        animatedGOs.Remove(GO);
    }
    #endregion


    #region COLOR
    /**
     *      --- COLOR ---
     */
    public void ChangeColor(GameObject GO, Color targetColor, float time)
    {
        if (animatedGOs.ContainsKey(GO) == true)
            return;

        animatedGOs.Add(GO, this.StartCoroutine(ChangeColorCoroutine(GO, targetColor, time)));
    }
    IEnumerator ChangeColorCoroutine(GameObject GO, Color targetColor, float time)
    {
        // store here all renderers with their initial colors of the object and its children
        Dictionary<ColorRenderer, Color> renderers = new Dictionary<ColorRenderer, Color>();


        // add all renderers of the object and its children
        Renderer[] rendererComponents = GO.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in rendererComponents)
        {
            ColorRenderer colorRenderer = new ColorRenderer();
            if (colorRenderer.SetRenderer(renderer) == true)
            {
                renderers.Add(colorRenderer, colorRenderer.GetColor());
            }
        }

        // animated all renderers from 
        for (float currTime = 0; currTime <= time; currTime += Time.deltaTime)
        {
            float normalizedTime = ConvertRangeClamped(0, time, 0, 1, currTime);
            foreach (ColorRenderer cr in renderers.Keys)
            {
                Color origColor = renderers[cr];
                cr.SetColor(Color.Lerp(origColor, targetColor, normalizedTime));
            }
            yield return null;
        }
        
        // go to target color 100%
        foreach (ColorRenderer cr in renderers.Keys)
        {
            Color origColor = renderers[cr];
            cr.SetColor(targetColor);
        }

        animatedGOs.Remove(GO);
    }
    #endregion


    class ColorRenderer
    {
        Object renderer;
        public bool SetRenderer(Object renderer)
        {
            if (typeof(Renderer).IsAssignableFrom(renderer.GetType()))
            {
                if (((Renderer)renderer).material.HasProperty("_Color") == false)
                    return false;
            }

            this.renderer = renderer;
            return true;
        }
		public void SetRendererWithoutColorCheck(Object renderer)
		{
			this.renderer = renderer;
		}
		public Object GetRenderer()
        {
            return renderer;
        }

        public void SetColor(Color color)
        {
            if (renderer == null)
                return;

            else if(typeof(Renderer).IsAssignableFrom(renderer.GetType()))
                ((Renderer)renderer).material.color = color;
        }

        public Color GetColor()
        {
            if (renderer == null)
                return Color.white;

            else if(typeof(Renderer).IsAssignableFrom(renderer.GetType()))
                return ((Renderer)renderer).material.color;

            // should return null..
            return Color.white;
        }

        public void SetMaterial(Material material)
        {
            if (renderer == null)
                return;

            if (typeof(Renderer).IsAssignableFrom(renderer.GetType()))
                ((Renderer)renderer).material = material;
        }
        public Material GetMaterial()
        {
            if (renderer == null)
                return null;

            else if(typeof(Renderer).IsAssignableFrom(renderer.GetType()))
                return ((Renderer)renderer).material;

            return null;
        }

        public Bounds? GetBounds()
        {
            if (renderer == null)
                return null;

            else if (typeof(Renderer).IsAssignableFrom(renderer.GetType()))
                return ((Renderer)renderer).bounds;

            return null;
        }
    }

    void OnDestroy()
    {
        // stop all coroutines
        foreach(var item in animatedGOs.Values)
        {
            if(item != null)
                StopCoroutine(item);
        }

        animatedGOs.Clear();
    }

    public static float ConvertRange(float originalStart, float originalEnd, float newStart, float newEnd, float value)
    {
        return (((value - originalStart) * (newEnd - newStart)) / (originalEnd - originalStart)) + newStart;
    }
    public static float ConvertRangeClamped(float originalStart, float originalEnd, float newStart, float newEnd, float value)
    {
        if (value < originalStart)
            value = originalStart;
        if (value > originalEnd)
            value = originalEnd;
        return ConvertRange(originalStart, originalEnd, newStart, newEnd, value);
    }
}
