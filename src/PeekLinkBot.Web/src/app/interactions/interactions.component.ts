import { Component, signal, Signal } from '@angular/core';

interface Comment {
  redditId: string;
  createdAtUtc: string;
  username: string;
  content: string;
}

interface Interaction {
  id: string;
  originalComment: Comment;
  triggerComment: Comment;
  replyComment: Comment;
}

@Component({
  selector: 'app-interactions',
  imports: [],
  templateUrl: './interactions.component.html',
  styleUrl: './interactions.component.scss'
})
export class InteractionsComponent {
  interactions: Signal<Interaction[]> = signal([
    {
      id: '1',
      originalComment: {
        redditId: 'abc123',
        createdAtUtc: '2023-10-01T12:00:00Z',
        username: 'user1',
        content: 'This is the original comment.'
      },
      triggerComment: {
        redditId: 'def456',
        createdAtUtc: '2023-10-01T12:05:00Z',
        username: 'user2',
        content: 'This is a trigger comment.'
      },
      replyComment: {
        redditId: 'ghi789',
        createdAtUtc: '2023-10-01T12:10:00Z',
        username: 'PeekLinkBot',
        content: 'This is a reply to the trigger comment.'
      }
    },
    {
      id: '2',
      originalComment: {
        redditId: 'jkl012',
        createdAtUtc: '2023-10-02T14:00:00Z',
        username: 'user3',
        content: 'Another original comment.'
      },
      triggerComment: {
        redditId: 'mno345',
        createdAtUtc: '2023-10-02T14:05:00Z',
        username: 'user4',
        content: 'This is another trigger comment.'
      },
      replyComment: {
        redditId: 'pqr678',
        createdAtUtc: '2023-10-02T14:10:00Z',
        username: 'PeekLinkBot',
        content: 'This is a reply to the second trigger comment.'
      }
    },
    {
      id: '3',
      originalComment: {
        redditId: 'stu901',
        createdAtUtc: '2023-10-03T16:00:00Z',
        username: 'user5',
        content: 'Yet another original comment.'
      },
      triggerComment: {
        redditId: 'vwx234',
        createdAtUtc: '2023-10-03T16:05:00Z',
        username: 'user6',
        content: 'This is yet another trigger comment.'
      },
      replyComment: {
        redditId: 'yz1234',
        createdAtUtc: '2023-10-03T16:10:00Z',
        username: 'PeekLinkBot',
        content: 'This is a reply to the third trigger comment.'
      }
    }
  ]);
}
