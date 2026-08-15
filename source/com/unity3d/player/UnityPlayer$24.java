package com.unity3d.player;

import java.util.concurrent.Semaphore;

final class UnityPlayer$24 implements Runnable {
    final Semaphore a;
    final UnityPlayer b;

    UnityPlayer$24(UnityPlayer unityPlayer, Semaphore semaphore) {
        this.b = unityPlayer;
        this.a = semaphore;
    }

    @Override
    public final void run() {
        if (!UnityPlayer.access$1900(this.b)) {
            this.a.release();
            return;
        }
        UnityPlayer.access$2002(this.b, true);
        UnityPlayer.access$1800(this.b);
        this.a.release(2);
    }
}
