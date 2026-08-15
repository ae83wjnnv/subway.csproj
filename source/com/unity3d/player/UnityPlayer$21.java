package com.unity3d.player;

import android.view.Surface;
import java.util.concurrent.Semaphore;

final class UnityPlayer$21 implements Runnable {
    final int a;
    final Surface b;
    final Semaphore c;
    final UnityPlayer d;

    UnityPlayer$21(UnityPlayer unityPlayer, int i, Surface surface, Semaphore semaphore) {
        this.d = unityPlayer;
        this.a = i;
        this.b = surface;
        this.c = semaphore;
    }

    @Override
    public final void run() {
        UnityPlayer.access$1600(this.d, this.a, this.b);
        this.c.release();
    }
}
