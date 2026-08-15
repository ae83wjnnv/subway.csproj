package com.unity3d.player;

import android.graphics.Rect;

final class UnityPlayer$12 extends UnityPlayer$f {
    final Rect a;
    final UnityPlayer b;

    UnityPlayer$12(UnityPlayer unityPlayer, Rect rect) {
        super(unityPlayer, (byte) 0);
        this.b = unityPlayer;
        this.a = rect;
    }

    @Override
    public final void a() {
        UnityPlayer.access$3100(this.b, this.a.left, this.a.top, this.a.right, this.a.bottom);
    }
}
