package com.unity3d.player;

final class UnityPlayer$3$1 implements Runnable {
    final UnityPlayer$3 a;

    UnityPlayer$3$1(UnityPlayer$3 unityPlayer$3) {
        this.a = unityPlayer$3;
    }

    @Override
    public final void run() {
        if (UnityPlayer.access$1200(this.a.a) != null) {
            UnityPlayer.access$1200(this.a.a).b(this.a.a);
        }
    }
}
