using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
[System.Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    private PlayableDirector playableDirector;

    public string characterName;
    [TextAreaAttribute(8, 1)] public string dialogueLine;
    public int dialogueSize;

    private bool isClipPlayed;//是否这个对话Clip片段，已经播放结束了
    public bool requeirePause;//用户设置：这个对话完成之后，是否需要玩家按下空格键才能继续对话
    private bool pauseScheduled;

    public override void OnPlayableCreate(Playable playable)
    {
        playableDirector = playable.GetGraph().GetResolver() as PlayableDirector;
    }
    //类似Mono Behavior中的Update方法，每一帧都在调用
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (isClipPlayed == false && info.weight > 0)
        {
            MovieManager.instance.SetDialogue(characterName, dialogueLine, dialogueSize);//将台词内容赋值到MovieManager当中

            if (requeirePause)
                pauseScheduled = true;
            isClipPlayed = true;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        isClipPlayed = false;
        Debug.Log("Clip is Stooooooop");
        if (pauseScheduled)
        {
            pauseScheduled = false;
            //Pause Timeline 暂停TimeLine的播放
            GameManager.instance.PauseTimeline(playableDirector);
        }
        else
        {
            MovieManager.instance.ToggleDialogueBox(false);
        }
    }
}
