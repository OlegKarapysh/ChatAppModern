import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'chat-test',
  templateUrl: './test.component.html',
  styleUrl: './test.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TestComponent {

}
