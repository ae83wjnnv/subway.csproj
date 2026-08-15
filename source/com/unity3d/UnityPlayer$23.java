package com.unity3d.player;

import java.util.concurrent.Semaphore;

final class UnityPlayer$23 implements Runnable {
    final Semaphore a;
    final UnityPlayer b;

    UnityPlayer$23(UnityPlayer unityPlayer, Semaphore semaphore) {
        this.b = unityPlayer;
        this.a = semaphore;
    }

    @Override
    public final void run() {
        UnityPlayer.access$1800(this.b);
        this.a.release();
    }
}
