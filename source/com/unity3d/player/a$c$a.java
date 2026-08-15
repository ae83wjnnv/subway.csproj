package com.unity3d.player;

class a$c$a implements Runnable {
    private IAssetPackManagerMobileDataConfirmationCallback a;
    private boolean b;

    a$c$a(IAssetPackManagerMobileDataConfirmationCallback iAssetPackManagerMobileDataConfirmationCallback, boolean z) {
        this.a = iAssetPackManagerMobileDataConfirmationCallback;
        this.b = z;
    }

    @Override
    public final void run() {
        this.a.onMobileDataConfirmationResult(this.b);
    }
}
