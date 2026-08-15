package com.google.androidgamesdk;

import android.view.Choreographer;
import android.view.Choreographer$FrameCallback;

public class ChoreographerCallback implements Choreographer$FrameCallback {
    private static final String LOG_TAG = "ChoreographerCallback";
    private long mCookie;
    private ChoreographerCallback$a mLooper;

    public ChoreographerCallback(long j) {
        this.mCookie = j;
        ChoreographerCallback$a choreographerCallback$a = new ChoreographerCallback$a(this, (byte) 0);
        this.mLooper = choreographerCallback$a;
        choreographerCallback$a.start();
    }

    @Override
    public void doFrame(long j) {
        nOnChoreographer(this.mCookie, j);
    }

    public native void nOnChoreographer(long j, long j2);

    public void postFrameCallback() {
        this.mLooper.a.post(new ChoreographerCallback$1(this));
    }

    public void postFrameCallbackDelayed(long j) {
        Choreographer.getInstance().postFrameCallbackDelayed(this, j);
    }

    public void terminate() {
        this.mLooper.a.getLooper().quit();
    }
}
