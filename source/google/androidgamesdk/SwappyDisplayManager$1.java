package com.google.androidgamesdk;

import android.view.Window;
import android.view.WindowManager$LayoutParams;

final class SwappyDisplayManager$1 implements Runnable {
    final int a;
    final SwappyDisplayManager b;

    SwappyDisplayManager$1(SwappyDisplayManager swappyDisplayManager, int i) {
        this.b = swappyDisplayManager;
        this.a = i;
    }

    @Override
    public final void run() {
        Window window = SwappyDisplayManager.access$100(this.b).getWindow();
        WindowManager$LayoutParams attributes = window.getAttributes();
        attributes.preferredDisplayModeId = this.a;
        window.setAttributes(attributes);
    }
}
