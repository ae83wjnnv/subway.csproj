package com.unity3d.player;

import android.graphics.Point;
import android.graphics.Rect;
import android.view.View;
import android.view.ViewTreeObserver$OnGlobalLayoutListener;

final class i$1 implements ViewTreeObserver$OnGlobalLayoutListener {
    final View a;
    final i b;

    i$1(i iVar, View view) {
        this.b = iVar;
        this.a = view;
    }

    @Override
    public final void onGlobalLayout() {
        if (this.a.isShown()) {
            Rect rect = new Rect();
            i.a(this.b).getWindowVisibleDisplayFrame(rect);
            int[] iArr = new int[2];
            i.a(this.b).getLocationOnScreen(iArr);
            Point point = new Point(rect.left - iArr[0], rect.height() - this.a.getHeight());
            Point point2 = new Point();
            this.b.getWindow().getWindowManager().getDefaultDisplay().getSize(point2);
            int height = i.a(this.b).getHeight() - point2.y;
            int height2 = i.a(this.b).getHeight() - point.y;
            if (height2 != height + this.a.getHeight()) {
                i.a(this.b).reportSoftInputIsVisible(true);
            } else {
                i.a(this.b).reportSoftInputIsVisible(false);
            }
            i.a(this.b).reportSoftInputArea(new Rect(point.x, point.y, this.a.getWidth(), height2));
        }
    }
}
