package com.unity3d.player;

final class o$4 implements Runnable {
    final o a;

    o$4(o oVar) {
        this.a = oVar;
    }

    @Override
    public final void run() {
        o.f(this.a);
        o.g(this.a).resume();
    }
}
