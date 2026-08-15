package com.google.androidgamesdk;

import android.os.Handler;
import android.os.Looper;
import android.util.Log;

class ChoreographerCallback$a extends Thread {
    public Handler a;
    final ChoreographerCallback b;

    private ChoreographerCallback$a(ChoreographerCallback choreographerCallback) {
        this.b = choreographerCallback;
    }

    ChoreographerCallback$a(ChoreographerCallback choreographerCallback, byte b) {
        this(choreographerCallback);
    }

    @Override
    public final void run() {
        Log.i("ChoreographerCallback", "Starting looper thread");
        Looper.prepare();
        this.a = new Handler();
        Looper.loop();
        Log.i("ChoreographerCallback", "Terminating looper thread");
    }
}
