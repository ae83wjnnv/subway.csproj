package com.unity3d.player;

final class o$3 implements Runnable {
    final o a;

    o$3(o oVar) {
        this.a = oVar;
    }

    @Override
    public final void run() {
        if (o.a(this.a) != null) {
            o.g(this.a).addViewToPlayer(o.a(this.a), true);
            o.h(this.a);
            o.a(this.a).requestFocus();
        }
    }
}
