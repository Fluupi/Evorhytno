using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ButtonClip : PlayableAsset
{
    public BtnValue btn;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ButtonReceiver>.Create(graph);

        var buttonReceiver = playable.GetBehaviour();
        buttonReceiver.Btn = btn;

        return playable;
    }
}
