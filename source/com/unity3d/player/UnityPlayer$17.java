package com.unity3d.player;

import android.content.Context;
import android.view.OrientationEventListener;

final class UnityPlayer$17 extends OrientationEventListener {
    final UnityPlayer a;

    UnityPlayer$17(UnityPlayer unityPlayer, Context context, int i) {
        super(context, i);
        this.a = unityPlayer;
    }

    @Override
    public final void onOrientationChanged(int i) {
        this.a.m_MainThread.a(UnityPlayer.access$3800(this.a), i);
    }
}
