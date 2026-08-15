package com.unity3d.player;

import android.graphics.SurfaceTexture;
import android.graphics.SurfaceTexture$OnFrameAvailableListener;

final class c$5 implements SurfaceTexture$OnFrameAvailableListener {
    final c a;

    c$5(c cVar) {
        this.a = cVar;
    }

    @Override
    public final void onFrameAvailable(SurfaceTexture surfaceTexture) {
        c.h(this.a).a(surfaceTexture);
    }
}
