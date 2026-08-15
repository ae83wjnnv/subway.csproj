package com.unity3d.player;

final class o$2 implements Runnable {
    final o a;

    o$2(o oVar) {
        this.a = oVar;
    }

    @Override
    public final void run() {
        o.g(this.a).pause();
    }
}
