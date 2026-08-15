package com.unity3d.player;

public class n$b implements Runnable {
    final n a;
    private n b;
    private boolean c = false;

    public n$b(n nVar, n nVar2) {
        this.a = nVar;
        this.b = nVar2;
    }

    public final void a() {
        this.c = true;
    }

    @Override
    public final void run() {
        try {
            Thread.sleep(5000L);
        } catch (InterruptedException unused) {
            Thread.currentThread().interrupt();
        }
        if (this.c) {
            return;
        }
        if (n.b()) {
            n.a("Stopping the video player due to timeout.");
        }
        this.b.CancelOnPrepare();
    }
}
