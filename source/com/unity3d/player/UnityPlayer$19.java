package com.unity3d.player;

import android.view.SurfaceHolder;
import android.view.SurfaceHolder$Callback;

final class UnityPlayer$19 implements SurfaceHolder$Callback {
    final UnityPlayer a;

    UnityPlayer$19(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
        UnityPlayer.access$1100(this.a, 0, surfaceHolder.getSurface());
        UnityPlayer.access$1300(this.a);
    }

    @Override
    public final void surfaceCreated(SurfaceHolder surfaceHolder) {
        UnityPlayer.access$1100(this.a, 0, surfaceHolder.getSurface());
        if (UnityPlayer.access$1200(this.a) != null) {
            UnityPlayer.access$1200(this.a).a(this.a);
        }
    }

    @Override
    public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
        if (UnityPlayer.access$1200(this.a) != null) {
            UnityPlayer.access$1200(this.a).a(UnityPlayer.access$1400(this.a));
        }
        UnityPlayer.access$1100(this.a, 0, null);
    }
}
