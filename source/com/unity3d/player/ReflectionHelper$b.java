package com.unity3d.player;

class ReflectionHelper$b implements Runnable {
    final long a;
    final long b;

    public ReflectionHelper$b(long j, long j2) {
        this.a = j;
        this.b = j2;
    }

    @Override
    public final void run() {
        if (ReflectionHelper.beginProxyCall(this.a)) {
            try {
                ReflectionHelper.b(this.b);
            } finally {
                ReflectionHelper.endProxyCall();
            }
        }
    }
}
