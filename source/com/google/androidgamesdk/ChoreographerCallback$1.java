package com.google.androidgamesdk;

import android.view.Choreographer;

final class ChoreographerCallback$1 implements Runnable {
    final ChoreographerCallback a;

    ChoreographerCallback$1(ChoreographerCallback choreographerCallback) {
        this.a = choreographerCallback;
    }

    @Override
    public final void run() {
        Choreographer.getInstance().postFrameCallback(this.a);
    }
}
