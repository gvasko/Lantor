export interface Action {
  (): void;
}

export interface ActionT<T> {
  (data: T): void;
}

