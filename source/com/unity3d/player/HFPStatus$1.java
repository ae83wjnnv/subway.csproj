package com.unity3d.player;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

class HFPStatus$1 extends BroadcastReceiver {
    final HFPStatus a;

    HFPStatus$1(HFPStatus hFPStatus) {
        this.a = hFPStatus;
    }

    @Override
    public void onReceive(Context context, Intent intent) {
        if (intent.getIntExtra("android.media.extra.SCO_AUDIO_STATE", -1) != 1) {
            return;
        }
        HFPStatus.a(this.a, HFPStatus$a.b);
        HFPStatus.a(this.a);
        if (HFPStatus.b(this.a)) {
            HFPStatus.c(this.a).setMode(3);
        }
    }
}
