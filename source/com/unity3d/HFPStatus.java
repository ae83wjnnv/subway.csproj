package com.unity3d.player;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.media.AudioManager;

public class HFPStatus {
    private Context a;
    private AudioManager e;
    private BroadcastReceiver b = null;
    private Intent c = null;
    private boolean d = false;
    private boolean f = false;
    private int g = HFPStatus$a.a;

    public HFPStatus(Context context) {
        this.e = null;
        this.a = context;
        this.e = (AudioManager) context.getSystemService("audio");
        initHFPStatusJni();
    }

    static int a(HFPStatus hFPStatus, int i) {
        hFPStatus.g = i;
        return i;
    }

    static void a(HFPStatus hFPStatus) {
        hFPStatus.c();
    }

    private void b() {
        BroadcastReceiver broadcastReceiver = this.b;
        if (broadcastReceiver != null) {
            this.a.unregisterReceiver(broadcastReceiver);
            this.b = null;
            this.c = null;
        }
        this.g = HFPStatus$a.a;
    }

    static boolean b(HFPStatus hFPStatus) {
        return hFPStatus.d;
    }

    static AudioManager c(HFPStatus hFPStatus) {
        return hFPStatus.e;
    }

    private void c() {
        if (this.f) {
            this.f = false;
            this.e.stopBluetoothSco();
        }
    }

    private final native void deinitHFPStatusJni();

    private final native void initHFPStatusJni();

    public final void a() {
        clearHFPStat();
        deinitHFPStatusJni();
    }

    protected void clearHFPStat() {
        b();
        c();
    }

    protected boolean getHFPStat() {
        return this.g == HFPStatus$a.b;
    }

    protected void requestHFPStat() {
        clearHFPStat();
        HFPStatus$1 hFPStatus$1 = new HFPStatus$1(this);
        this.b = hFPStatus$1;
        this.c = this.a.registerReceiver(hFPStatus$1, new IntentFilter("android.media.ACTION_SCO_AUDIO_STATE_UPDATED"));
        try {
            this.f = true;
            this.e.startBluetoothSco();
        } catch (NullPointerException unused) {
            f.Log(5, "startBluetoothSco() failed. no bluetooth device connected.");
        }
    }

    protected void setHFPRecordingStat(boolean z) {
        this.d = z;
        if (z) {
            return;
        }
        this.e.setMode(0);
    }
}
