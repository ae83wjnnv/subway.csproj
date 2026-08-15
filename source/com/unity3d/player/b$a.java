package com.unity3d.player;

import android.database.ContentObserver;
import android.media.AudioManager;
import android.net.Uri;
import android.os.Handler;

class b$a extends ContentObserver {
    final b a;
    private final b$b b;
    private final AudioManager c;
    private final int d;
    private int e;

    public b$a(b bVar, Handler handler, AudioManager audioManager, int i, b$b b_b) {
        super(handler);
        this.a = bVar;
        this.c = audioManager;
        this.d = 3;
        this.b = b_b;
        this.e = audioManager.getStreamVolume(3);
    }

    @Override
    public final boolean deliverSelfNotifications() {
        return super.deliverSelfNotifications();
    }

    @Override
    public final void onChange(boolean z, Uri uri) {
        int streamVolume;
        AudioManager audioManager = this.c;
        if (audioManager == null || this.b == null || (streamVolume = audioManager.getStreamVolume(this.d)) == this.e) {
            return;
        }
        this.e = streamVolume;
        this.b.onAudioVolumeChanged(streamVolume);
    }
}
