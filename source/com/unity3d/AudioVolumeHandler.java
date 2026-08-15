package com.unity3d.player;

import android.content.Context;

public class AudioVolumeHandler implements b$b {
    private b a;

    AudioVolumeHandler(Context context) {
        b bVar = new b(context);
        this.a = bVar;
        bVar.a(this);
    }

    public final void a() {
        this.a.a();
        this.a = null;
    }

    @Override
    public final native void onAudioVolumeChanged(int i);
}
